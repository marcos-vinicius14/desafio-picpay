namespace Picpay_01.DTOs;

public class TransactionDto()
{
   public double Value { get; set; }

   public int SenderId { get; set; }

   public int ReceiverId { get; set; }

   public TransactionDto(double value, int senderId, int receiverId) : this()
   {
      Value = value;
      SenderId = senderId;
      ReceiverId = receiverId;
   }
   // public void Record(double value, int senderId, int receiverId) {}
}