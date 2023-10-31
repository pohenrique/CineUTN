namespace Web.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsUrgent { get; set; }
        public virtual List<PedidoItem> Items { get; set; }
        public DateTime Created { get; set; }

        //This should be in ViewModel
        public int NumberOfItems
        {
            get => Items.Count;
        }

        public Pedido()
        {
            Items = new List<PedidoItem>();
        }
    }
}
