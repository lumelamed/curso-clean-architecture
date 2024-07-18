﻿namespace CleanArchitecture.Infrastructure.Configurations
{
    using CleanArchitecture.Domain.Alquileres;
    using CleanArchitecture.Domain.Reviews;
    using CleanArchitecture.Domain.Users;
    using CleanArchitecture.Domain.Vehiculos;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("reviews");

            builder.HasKey(review => review.Id);

            builder.Property(review => review.Rating)
                .HasConversion(rating => rating.Value, value => Rating.Create(value).Value);

            builder.Property(review => review.Comentario)
                .HasConversion(comentario => comentario!.value, value => new Comentario(value));

            builder.HasOne<Vehiculo>().WithMany().HasForeignKey(review => review.VehiculoId);

            builder.HasOne<Alquiler>().WithMany().HasForeignKey(review => review.AlquilerId);

            builder.HasOne<User>().WithMany().HasForeignKey(review => review.UserId);
        }
    }
}