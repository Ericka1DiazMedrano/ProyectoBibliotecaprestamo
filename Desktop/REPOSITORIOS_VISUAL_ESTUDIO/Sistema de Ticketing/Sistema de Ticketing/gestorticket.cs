using Sistema_de_Ticketing;
using System;
using TicketsSoporte.logica;

public class GestorTicket
{
    public List<Tecnico> lstTecnicos { get; set; }
    public List<Solicitante> lstSolicitante { get; set; }
    public List<Ticket> lstTickets { get; set; }
    public GestorTicket()
    {
        lstTecnicos = new List<Tecnico>();
        lstSolicitante = new List<Solicitante>();
        lstTickets = new List<Ticket>(); }
    public Ticket crearTicket(int intNumero, string strTitulo, string strDescripcion, string strCategoria, string strPrioridad, Solicitante objSolicitante)
    {
        Ticket objTicket = new Ticket(intNumero, strTitulo, strDescripcion, strCategoria, strPrioridad, objSolicitante);
        lstTickets.Add(objTicket);
        return objTicket;

    }
}