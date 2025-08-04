using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RoleConfig : IEntityTypeConfiguration<RoleDto>
    {
        public void Configure(EntityTypeBuilder<RoleDto> builder)
        {
            builder.HasData(
                new RoleDto { Id = 1, RoleName = 0, PersonId = 1 },
                new RoleDto { Id = 2, RoleName = 1, PersonId = 2 },
                new RoleDto { Id = 3, RoleName = 2, PersonId = 3 }
            );
        }
    }
}
