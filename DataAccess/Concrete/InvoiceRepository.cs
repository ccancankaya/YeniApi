using DataAccess.Abstract;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private DataContext _context;
        public InvoiceRepository(DataContext context)
        {
            _context = context;
        }
        public void CreateInvoice(Invoice ınvoice)
        {
            _context.Invoices.Add(ınvoice);
        }

        public void DeleteInvoice(Invoice ınvoice)
        {
            _context.Invoices.Remove(ınvoice);
        }

        public Invoice GetInvoiceById(int id)
        {
            return _context.Invoices.FirstOrDefault(ınvoice => ınvoice.Id.Equals(id));
        }

        public Invoice GetInvoiceByOrderId(int orderId)
        {
            return _context.Invoices.FirstOrDefault(ınvoice => ınvoice.OrderId.Equals(orderId));
        }

        public void UpdateInvoice(Invoice ınvoice)
        {
            _context.Invoices.Update(ınvoice);
        }
    }
}
