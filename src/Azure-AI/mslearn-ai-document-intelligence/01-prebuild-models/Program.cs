//using Azure;
//using Azure.AI.FormRecognizer.DocumentAnalysis;

//// Store connection information
//string endpoint = "<Endpoint URL>";
//string apiKey = "<API Key>";

//Uri fileUri = new Uri("https://github.com/MicrosoftLearning/mslearn-ai-document-intelligence/blob/main/Labfiles/01-prebuild-models/sample-invoice/sample-invoice.pdf?raw=true");

//Console.WriteLine("\nConnecting to Forms Recognizer at: {0}", endpoint);
//Console.WriteLine("Analyzing invoice at: {0}\n", fileUri.ToString());


//// Create the client




//// Analyze the invoice




//// Display invoice information to the user


//    if (invoice.Fields.TryGetValue("CustomerName", out DocumentField? customerNameField))
//    {
//        if (customerNameField.FieldType == DocumentFieldType.String)
//        {
//            string customerName = customerNameField.Value.AsString();
//            Console.WriteLine($"Customer Name: '{customerName}', with confidence {customerNameField.Confidence}.");
//        }
//    }
    
//    if (invoice.Fields.TryGetValue("InvoiceTotal", out DocumentField? invoiceTotalField))
//    {
//        if (invoiceTotalField.FieldType == DocumentFieldType.Currency)
//        {
//            CurrencyValue invoiceTotal = invoiceTotalField.Value.AsCurrency();
//            Console.WriteLine($"Invoice Total: '{invoiceTotal.Symbol}{invoiceTotal.Amount}', with confidence {invoiceTotalField.Confidence}.");
//        }
//    }
//}
//Console.WriteLine("\nAnalysis complete.\n");