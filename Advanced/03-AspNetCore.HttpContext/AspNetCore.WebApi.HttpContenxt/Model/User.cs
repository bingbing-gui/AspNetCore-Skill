namespace AspNetCore.WebApi.HttpContenxt.Model
{
    public class User
    {
        /// <summary>
        /// 
        /// </summary>
        public Int64? Id
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string? UserName
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string? PassWord
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string? Salt
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string? Email
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool? Validate
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ModifyTime
        {
            get;
            set;
        }
    }
}
