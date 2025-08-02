using FluentMigrator;

namespace PracticeBuddy.DbMigrations.Migrations;

[Migration(20250713)]
public class CreateTables : AutoReversingMigration
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
            .WithColumn("email").AsString(200).NotNullable();
        // .WithColumn("authzeroid").AsString(200).Nullable()

        Create.Schema("exerc");
        Create.Table("routine").InSchema("exerc")
            .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("user_id").AsInt32().NotNullable() // FK
            .WithColumn("practice_count").AsInt32().Nullable()
            .WithColumn("last_practiced_at").AsDateTime().Nullable()
            .WithColumn("created_at").AsDateTime().Nullable()
            .WithColumn("last_updated_at").AsDateTime().Nullable();

        Create.Table("exercise").InSchema("exerc")
            .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("goal_bpm").AsInt32().NotNullable()
            .WithColumn("user_id").AsInt32().NotNullable()
            .WithColumn("practice_count").AsInt32().NotNullable()
            .WithColumn("last_practiced_at").AsDateTime().Nullable()
            .WithColumn("created_at").AsDateTime().Nullable()
            .WithColumn("last_updated_at").AsDateTime().Nullable()
            .WithColumn("routine_id").AsInt32().NotNullable(); // FK

        Create.Table("exercise_instance").InSchema("exerc")
            .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("practice_count").AsInt32().NotNullable()
            .WithColumn("reached_goal").AsBoolean().NotNullable()
            .WithColumn("exercise_id").AsInt32().NotNullable(); // FK

        Create.Schema("pract");
        Create.Table("practice_session").InSchema("pract")
            .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("routine_id").AsInt32().NotNullable() // FK
            .WithColumn("statisfaction_level").AsString().NotNullable()
            .WithColumn("practiced_at").AsDateTime().Nullable()
            .WithColumn("notes").AsString().Nullable();

        Create.Table("practice_instance").InSchema("pract")
            .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("exercise_instance_id").AsInt32().NotNullable() // FK
            .WithColumn("max_bpm").AsInt32().Nullable()
            .WithColumn("comfortable_bpm").AsInt32().Nullable()
            .WithColumn("statisfaction_level").AsString().NotNullable()
            .WithColumn("practiced_at").AsDateTime().Nullable()
            .WithColumn("notes").AsString().Nullable();

        Create.ForeignKey()
            .FromTable("routine").ForeignColumn("user_id")
            .ToTable("user").PrimaryColumn("id");

        Create.ForeignKey()
            .FromTable("exercise").ForeignColumn("routine_id")
            .ToTable("routine").PrimaryColumn("id");

        Create.ForeignKey()
            .FromTable("exercise_instance").ForeignColumn("exercise_id")
            .ToTable("exercise").PrimaryColumn("id");

        Create.ForeignKey()
            .FromTable("practice_session").ForeignColumn("routine_id")
            .ToTable("routine").PrimaryColumn("id");
            
        Create.ForeignKey()
            .FromTable("practice_instance").ForeignColumn("exercise_instance_id")
            .ToTable("exercise_instance").PrimaryColumn("id");
    }

    // public override void Down()
    // {
    //     Delete.ForeignKey().FromTable("routine");
    //     Delete.ForeignKey().FromTable("exercise");
    //     Delete.ForeignKey().FromTable("exercise_instance");
    //     Delete.ForeignKey().FromTable("practice_session");
    //     Delete.ForeignKey().FromTable("practice_instance");

    //     Delete.Table("user");
    //     Delete.Table("routine");
    //     Delete.Table("exercise");
    //     Delete.Table("exercise_instance");
    //     Delete.Table("practice_session");
    //     Delete.Table("practice_instance");
    // }
}