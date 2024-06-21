﻿// <auto-generated />
using System;
using Messegify.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Messegify.Application.Migrations
{
    [DbContext(typeof(MessegifyDbContext))]
    partial class MessegifyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Messegify.Domain.Entities.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Messegify.Domain.Entities.AccountChatroom", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateJoined")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("RoomId");

                    b.ToTable("AccountRooms");
                });

            modelBuilder.Entity("Messegify.Domain.Entities.Chatroom", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ChatRoomType")
                        .IsRequired()
                        .HasColumnType("nvarchar(24)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Messegify.Domain.Entities.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("ContactChatRoomId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("FirstAccountId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("SecondAccountId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ContactChatRoomId");

                    b.HasIndex("FirstAccountId");

                    b.HasIndex("SecondAccountId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("Messegify.Domain.Entities.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ChatRoomId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("SentDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("TextContent")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("ChatRoomId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Messegify.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Messegify.Domain.Entities.AccountChatroom", b =>
                {
                    b.HasOne("Messegify.Domain.Entities.Account", "Account")
                        .WithMany("AccountRooms")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Messegify.Domain.Entities.Chatroom", "Chatroom")
                        .WithMany("Members")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Chatroom");
                });

            modelBuilder.Entity("Messegify.Domain.Entities.Contact", b =>
                {
                    b.HasOne("Messegify.Domain.Entities.Chatroom", "ContactChatroom")
                        .WithMany()
                        .HasForeignKey("ContactChatRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Messegify.Domain.Entities.Account", "FirstAccount")
                        .WithMany()
                        .HasForeignKey("FirstAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Messegify.Domain.Entities.Account", "SecondAccount")
                        .WithMany()
                        .HasForeignKey("SecondAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ContactChatroom");

                    b.Navigation("FirstAccount");

                    b.Navigation("SecondAccount");
                });

            modelBuilder.Entity("Messegify.Domain.Entities.Message", b =>
                {
                    b.HasOne("Messegify.Domain.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Messegify.Domain.Entities.Chatroom", "Chatroom")
                        .WithMany("Messages")
                        .HasForeignKey("ChatRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Chatroom");
                });

            modelBuilder.Entity("Messegify.Domain.Entities.User", b =>
                {
                    b.HasOne("Messegify.Domain.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Messegify.Domain.Entities.Account", b =>
                {
                    b.Navigation("AccountRooms");
                });

            modelBuilder.Entity("Messegify.Domain.Entities.Chatroom", b =>
                {
                    b.Navigation("Members");

                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
