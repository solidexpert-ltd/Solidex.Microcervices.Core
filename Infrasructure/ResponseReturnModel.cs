using System.Net;
using System.Runtime.Serialization;

namespace Microcervices.Core.Infrasructure
{
    /// <summary>
    /// Response view model for api methods
    /// </summary>
    [DataContract]
    public class ResponseReturnModel
    {
        [DataMember(Name = "isSuccess")]
        public bool IsSuccess { get; set; } = true;
        [DataMember(Name = "statusCode")]
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        [DataMember(Name = "message")]
        public string Message { get; set; } = string.Empty;

        #region Constructor

        public ResponseReturnModel()
        {

        }

        public ResponseReturnModel(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest, bool success = false)
        {
            IsSuccess = success;
            StatusCode = statusCode;
            Message = message;
        }

        #endregion


    }

    /// <summary>
    /// Данный класс может использоваться не только в контроллере, а в промежуточном слое и контроллер исходя из статуса отдаст экземпляр как захочет
    /// </summary>
    /// <typeparam name="TModel">Модель представления для фронта</typeparam>
    [DataContract]
    public class ResponseReturnModel<TModel> : ResponseReturnModel where TModel : class
    {
        [DataMember(Name = "viewModel")]
        public TModel ViewModel { get; set; }

        #region Constructor

        public ResponseReturnModel() : base()
        {

        }

        public ResponseReturnModel(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest, bool success = false) : base(message, statusCode, success)
        {

        }

        public ResponseReturnModel(TModel viewmodel): this()
        {
            ViewModel = viewmodel;
        }

        public ResponseReturnModel(TModel viewModel, string message, HttpStatusCode statusCode = HttpStatusCode.OK, bool success = true) : this(message, statusCode, success)
        {
            ViewModel = viewModel;
        }


        #endregion
    }

}
