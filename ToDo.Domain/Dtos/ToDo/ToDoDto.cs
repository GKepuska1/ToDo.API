namespace ToDo.Domain.Dtos.ToDo
{
    public class ToDoDtoGet : ToDoDtoCreate
    {
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateCompleted { get; set; }
    }

    public class ToDoDtoCreate
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class ToDoDtoUpdate : ToDoDtoCreate
    {
        public bool IsCompleted { get; set; }
    }
}
