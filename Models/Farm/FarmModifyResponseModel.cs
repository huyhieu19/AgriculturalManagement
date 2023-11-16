namespace Models.Farm
{
    public class FarmModifyResponseModel
    {
        public List<FarmDisplayModel>? farmDisplays { get; set; }
        public bool isSuccess { get; set; } = false;
    }
}
