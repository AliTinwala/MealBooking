﻿namespace MealBookingAPI.Application.DTOs
{
    public class NotificationRequestDTO
    {
        public Guid NotificationId { get; set; }
        public string? Message { get; set; }
        public bool isRead { get; set; }
    }
}
