namespace TesteApiGmillView.Models
{
    public class EmployeeProject
    {
        public Project Project { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
        public EmployeeProject()
        {
                
        }
    }
}
