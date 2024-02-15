using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Config
{
    public class ReservationConfig : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasData(
                new Reservation { Id = 1, Book = "Karagöz ve Hacivat", Customer = "Nihat Kahraman", BorrowDate = DateTime.Now, BorrowEndDate = DateTime.Now }
                );
        }
    }
}
