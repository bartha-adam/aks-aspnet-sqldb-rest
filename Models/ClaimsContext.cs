using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClaimsApi.Models {
    public sealed class ClaimsContext : DbContext {
        private readonly ILogger<ClaimsContext> _logger;

        /// <inheritdoc />
        public ClaimsContext(DbContextOptions<ClaimsContext> options, ILogger<ClaimsContext> logger) : base(options)
        {
            _logger = logger;
            logger.LogInformation($"DB connection string = {Database.GetDbConnection().ConnectionString}");
        }
        public DbSet<ClaimItem> ClaimItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            
            if (modelBuilder == null)
            {
                throw new System.ArgumentNullException(nameof(modelBuilder));
            };

            modelBuilder.HasSequence<int>("ClaimNumbers")
                .StartsAt(100)
                .IncrementsBy(1);

            modelBuilder.Entity<ClaimItem>().ToTable("Claims");

            modelBuilder.Entity<ClaimItem>().Property(b => b.ClaimItemId).HasColumnName("claim_item_id");
            modelBuilder.Entity<ClaimItem>()
                .Property(b => b.ClaimItemId)
                .HasDefaultValueSql("NEXT VALUE FOR ClaimNumbers");

            modelBuilder.Entity<ClaimItem>().Property(b => b.ClaimStatus).HasColumnName("claim_status");
            modelBuilder.Entity<ClaimItem>().Property(b => b.ClaimType).HasColumnName("claim_type");
            modelBuilder.Entity<ClaimItem>().Property(b => b.SenderID).HasColumnName("sender_id");
            modelBuilder.Entity<ClaimItem>().Property(b => b.ReceiverID).HasColumnName("receiver_id");
            modelBuilder.Entity<ClaimItem>().Property(b => b.OriginatorID).HasColumnName("originator_id");
            modelBuilder.Entity<ClaimItem>().Property(b => b.DestinationID).HasColumnName("destination_id");
            modelBuilder.Entity<ClaimItem>().Property(b => b.ClaimInputMethod).HasColumnName("claim_inp_method");
            modelBuilder.Entity<ClaimItem>().Property(b => b.ClaimNumber).HasColumnName("claim_no");
            modelBuilder.Entity<ClaimItem>().Property(b => b.TotalClaimCharge).HasColumnType("decimal(10,2)");
            modelBuilder.Entity<ClaimItem>().Property(b => b.TotalClaimCharge).HasColumnName("total_claim_charge");
            modelBuilder.Entity<ClaimItem>().Property(b => b.PatientStatus).HasColumnName("patient_status");
            modelBuilder.Entity<ClaimItem>().Property(b => b.PatientAmountDue).HasColumnType("decimal(10,2)");
            modelBuilder.Entity<ClaimItem>().Property(b => b.PatientAmountDue).HasColumnName("patient_amt_due");
            modelBuilder.Entity<ClaimItem>().Property(b => b.ServiceDate).HasColumnName("service_date");
            modelBuilder.Entity<ClaimItem>().Property(b => b.PolicyNumber).HasColumnName("policy_no");
            modelBuilder.Entity<ClaimItem>().Property(b => b.ClaimPaidDate).HasColumnName("claim_paid_date");

            modelBuilder.HasSequence<int>("SubNumbers")
                .StartsAt(100)
                .IncrementsBy(1);
            modelBuilder.Entity<SubscriberInfo>()
                .Property(b => b.SubscriberInfoId)
                .HasDefaultValueSql("NEXT VALUE FOR SubNumbers");

            modelBuilder.HasSequence<int>("PlanPayNumbers")
                .StartsAt(100)
                .IncrementsBy(1);
            modelBuilder.Entity<PlanPayment>()
                .Property(b => b.PlanPaymentId)
                .HasDefaultValueSql("NEXT VALUE FOR PlanPayNumbers");

            modelBuilder.HasSequence<int>("ServiceLineNumbers")
                .StartsAt(100)
                .IncrementsBy(1);
            modelBuilder.Entity<ServiceLineDetails>()
                .Property(b => b.ServiceLineDetailsId)
                .HasDefaultValueSql("NEXT VALUE FOR ServiceLineNumbers");
        }
    }
}