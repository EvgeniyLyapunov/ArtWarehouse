namespace ArtWarehouse.Models.ErrorModels
{
    public class Error_Model
    {
        public Error_Model(string ex) 
        {
            ErrorMessage = ex;
        }
        public string ErrorMessage { get; set; }
    }
}
