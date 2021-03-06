﻿using System;

namespace Uncas.GraphiteAlerts.Models
{
    public class AlertResult
    {
        private readonly AlertLevel _level;
        private readonly DateTime? _timestamp;
        private readonly double _value;

        public AlertResult(
            AlertLevel level,
            double value,
            DateTime? timestamp,
            string comment)
        {
            _level = level;
            _value = value;
            _timestamp = timestamp;
            Comment = comment;
        }

        public AlertLevel Level
        {
            get { return _level; }
        }

        public double Value
        {
            get { return _value; }
        }

        public DateTime? Timestamp
        {
            get { return _timestamp; }
        }

        public string Comment { get; private set; }
    }
}