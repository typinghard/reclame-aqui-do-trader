﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ReclameAquiDoTrader.Business.Core.Messages
{
    public abstract class Command : Message
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public abstract bool EhValido();
    }
}