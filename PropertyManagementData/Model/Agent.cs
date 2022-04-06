using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PropertyManagementData.Model
{
    public class Agent
    {
        public int AgentId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Telephone { get; set; }

        public string Email { get; set; }
    }
}
