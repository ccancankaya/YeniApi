using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IInvoiceRepository 
    {
        Invoice GetInvoiceById(int id);
        Invoice GetInvoiceByOrderId(int orderId);
        void CreateInvoice(Invoice ınvoice);
        void UpdateInvoice(Invoice ınvoice);
        void DeleteInvoice(Invoice ınvoice);

    }
}
