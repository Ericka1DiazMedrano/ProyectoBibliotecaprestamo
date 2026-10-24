using Sistema_de_Ticketing;

namespace SoporteTicket.Logica
{
    public class GestorTicket
    {
        public List<Tecnico> LstTecnicos { get; set; }
        public List<Solicitante> LstSolicitantes { get; set; }
        public List<Ticket> LstTickets { get; set; }

        public GestorTicket()
        {
            LstTecnicos = new List<Tecnico>();
            LstSolicitantes = new List<Solicitante>();
            LstTickets = new List<Ticket>();
        }

        public Ticket CrearTicket(int intNumero, string strAsunto, string strDescripcion, string strCategoria, string strPrioridad, Solicitante objSolicitante, Tecnico objTecnico)
        {
            // Validaciones
            Ticket objTicket = new Ticket(intNumero, strAsunto, strDescripcion, strCategoria, strPrioridad, objSolicitante);
            objTicket.AsignarTecnico(objTecnico);

            LstTickets.Add(objTicket);
            return objTicket;
        }

        public Ticket BuscarTicket(int intTicket)
        {
            Ticket objTicket = LstTickets.Find(t => t.IntNumero == intTicket);

            if (objTicket == null)
            {
                Console.WriteLine("Ticket no existe");
            }

            return objTicket;
        }
    }
}