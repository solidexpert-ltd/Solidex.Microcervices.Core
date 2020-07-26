namespace Solidex.Core.ViewModels
{
    public class ResponseViewModel
    {
        public bool IsSuccess { get; set; } = true;
        public int StatusCode { get; set; } = 200;
        public string Message { get; set; } = string.Empty;

        public ResponseViewModel()
        {
            
        }

        public ResponseViewModel(string message, int status = 400, bool success = false)
        {
            IsSuccess = success;
            StatusCode = status;
            Message = message;
        }
    }

    public class ResponseViewModel<TModel>: ResponseViewModel where TModel: class
    {
        public TModel ViewModel { get; set; }

        public ResponseViewModel(TModel model, int status = 200, bool success = true)
        {
            ViewModel = model;
            IsSuccess = success;
            StatusCode = status;
        }

    }
}
