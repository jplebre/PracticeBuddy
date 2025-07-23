using FluentMigrator;

namespace PracticeBuddy.DbMigrations.Migrations;

[Migration(20250713)]
public class CreateRoutineTables : Migration
{
    public override void Up()
    {
        Create.Schema("usr");
        Create.Table("user").InSchema("usr")
            .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("username").AsString(100).NotNullable()
            .WithColumn("firstname").AsString(100).NotNullable()
            .WithColumn("lastname").AsString(100).NotNullable()
            .WithColumn("birthdate").AsDateTime().Nullable()
            .WithColumn("address").AsString(100).NotNullable()
            .WithColumn("email").AsString(200).Nullable();
        // user has routines and exercises

        Create.Schema("exerc");
        Create.Table("routine").InSchema("exerc")
            .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("last_practiced_at").AsDateTime().Nullable()
            .WithColumn("created_at").AsDateTime().Nullable()
            .WithColumn("last_updated_at").AsDateTime().Nullable()
            .WithColumn("practice_count").AsInt32().NotNullable();
        // exercises (foreign key)

        Create.Table("exercises").InSchema("exerc")
            .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("goal_bpm").AsInt32().NotNullable()
            .WithColumn("practice_count").AsInt32().NotNullable();
        // Keys
        // Modifiers

        Create.Table("exercise_instance").InSchema("exerc")
            .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("practice_count").AsInt32().NotNullable();
        // reached_goal?

        Create.Schema("pract");
        Create.Table("practice_instance").InSchema("pract")
            .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("max_bpm").AsInt32().Nullable()
            .WithColumn("comfortable_bpm").AsInt32().Nullable()
            .WithColumn("statisfaction_level").AsString().NotNullable()
            .WithColumn("practiced_at").AsDateTime().Nullable()
            .WithColumn("notes").AsString().Nullable();

		// Create.Table("TestTable2")
        // 	.WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
        // 	.WithColumn("Name").AsString(255).Nullable()
        // 	.WithColumn("TestTableId").AsInt32().NotNullable();
        //  .WithColumn("goal_bpm").AsFixedLengthString(10).NotNullable()
        //  .WithColumn("Price").AsDecimal(18, 2).NotNullable()
        //  .WithColumn("DiscountPercentage").AsInt32().Nullable();

        // Create.Index("ix_Name").OnTable("TestTable2").OnColumn("Name").Ascending()
        // 	.WithOptions().NonClustered();

        // Create.Column("Name2").OnTable("TestTable2").AsBoolean().Nullable();

        // Create.ForeignKey("fk_TestTable2_TestTableId_TestTable_Id")
        // 	.FromTable("TestTable2").ForeignColumn("TestTableId")
        // 	.ToTable("TestTable").PrimaryColumn("Id");

        // Insert.IntoTable("TestTable").Row(new { Name = "Test" });
    }

	public override void Down()
	{
		Delete.Table("user");
		Delete.Table("routine");
		Delete.Table("exercise");
		Delete.Table("exercise_instance");
		// Delete.Table("TestTable");
	}
}