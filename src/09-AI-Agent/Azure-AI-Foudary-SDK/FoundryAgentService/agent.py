import os
from dotenv import load_dotenv
from typing import Any
from pathlib import Path
import time
# Add references
from azure.identity import DefaultAzureCredential, EnvironmentCredential
from azure.ai.agents import AgentsClient
from azure.ai.agents.models import FilePurpose, CodeInterpreterTool, ListSortOrder, MessageRole

def main(): 
    # Clear the console
    os.system('cls' if os.name=='nt' else 'clear')
    # Load environment variables from .env file
    load_dotenv()
    project_endpoint= os.getenv("PROJECT_ENDPOINT")
    model_deployment = os.getenv("MODEL_DEPLOYMENT_NAME")
    # Display the data to be analyzed
    script_dir = Path(__file__).parent  # Get the directory of the script
    file_path = script_dir / 'data.txt'
    print(file_path)
    with file_path.open('r', encoding='utf-8') as file:
        data = file.read() + "\n"
        print(data)

    # 链接到Agent Client
    agent_client = AgentsClient(
        endpoint=project_endpoint,
        credential = EnvironmentCredential()
    )
    with agent_client:
        # 上传数据并创建一个CodeInterpreterTool
        file = agent_client.files.upload_and_poll(
        file_path=file_path, purpose=FilePurpose.AGENTS
        )
        print(f"Uploaded {file.filename}")
        code_interpreter = CodeInterpreterTool(file_ids=[file.id])

        # 创建一个agent 使用CodeInterpreterTool
        agent = agent_client.create_agent(
            model=model_deployment,
            name="data-agent",
            instructions="你是一个用于分析上传文件中数据的 AI 代理。如果用户请求图表，请创建图表并将其保存为 .png 文件。",
            tools=code_interpreter.definitions,
            tool_resources=code_interpreter.resources,
        )
        print(f"Using agent: {agent.name}")
        # Create a thread for the conversation
        thread = agent_client.threads.create()

        # Loop until the user types 'quit'
        while True:
            # Get input text
            user_prompt = input("Enter a prompt (or type 'quit' to exit): ")
            if user_prompt.lower() == "quit":
                break
            if len(user_prompt) == 0:
                print("Please enter a prompt.")
                continue
            # Send a prompt to the agent
            message = agent_client.messages.create(
                thread_id=thread.id,
                role="user",
                content=user_prompt,
            )
            run = agent_client.runs.create_and_process(thread_id=thread.id, agent_id=agent.id)
            print(f"Run ID: {run.status}")
            # ✅ 等待 run 完成
            # run = wait_for_run_completion(agent_client, thread.id, run.id)

            # Check the run status for failures
            if run.status == "failed":
                print(f"Run failed: {run.last_error}")
            # Show the latest response from the agent
            last_msg = agent_client.messages.get_last_message_text_by_role(
                thread_id=thread.id,
                role=MessageRole.AGENT,
            )
            if last_msg:
                print(f"Last Message: {last_msg.text.value}")
        # Get the conversation history
        print("\nConversation Log:\n")
        image_found = False
        messages = agent_client.messages.list(thread_id=thread.id, order=ListSortOrder.ASCENDING)
        for message in messages:
            if message.text_messages:
                last_msg = message.text_messages[-1]
                print(f"{message.role}: {last_msg.text.value}\n")
        # Get any generated files
        for msg in messages:
            # Save every image file in the message
            for img in msg.image_contents:
                file_id = img.image_file.file_id
                file_name = f"{file_id}_image_file.png"
                agent_client.files.save(file_id=file_id, file_name=file_name)
                image_found = True
                print(f"Saved image file to: {Path.cwd() / file_name}")
            if image_found:
                break
        if not image_found:
            print("⚠️ 没有找到任何图像消息，请确保 Agent 已成功生成图表。")
        # 清理资源
        agent_client.delete_agent(agent.id)

if __name__ == '__main__': 
    main()