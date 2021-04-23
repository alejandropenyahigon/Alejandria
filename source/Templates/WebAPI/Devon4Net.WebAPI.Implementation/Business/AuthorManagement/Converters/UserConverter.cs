using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Converters
{
    public static class UserConverter
    {

        public static UserDto ModelToDto(Users item)
        {
            if (item == null) return new UserDto();

            return new UserDto
            {
                UserId = item.UserId,
                Password = item.Password,
                UserRole = item.UserRole,
                AuthorId = item.AuthorId.ToString()
            };
        }
    }
}
