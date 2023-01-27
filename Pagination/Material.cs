namespace Pagination
{
    public class Material
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }

        public override string ToString()
        {
            return $"{Id,5}{Name,35}: {Count,6}";
        }
    }
}