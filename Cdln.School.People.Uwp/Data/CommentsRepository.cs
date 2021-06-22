using System;
using System.IO;
using School.People.Core;
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;
using School.People.Core.Attributes;
using Cdln.School.People.Uwp.Models;
using School.People.Core.Repositories;

namespace Cdln.School.People.Uwp.Data
{
    public class CommentsRepository : ICommentsRepository
    {
        public async Task<IComments> ReadAsync(Guid id)
        {
            try
            {
                string content = null;
                using (SqliteConnection connection = new SqliteConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqliteCommand cmd = new SqliteCommand($"SELECT [Comments] FROM CommentsTable WHERE [CommentsId] = @commentsId;", connection))
                    {
                        cmd.Parameters.AddWithValue("@commentsId", id);
                        using (SqliteDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.Read()) { content = reader.GetString(0); }
                        }
                    }
                    connection.Close();
                }
                return new Comments(id, content);
            }
            catch
            {
                // TODO: log exception
                return new Comments(id, null);
            }
        }

        public async Task<bool> UpdateAsync(IComments item)
        {
            try
            {
                if (item.Id == Guid.Empty) { return false; }
                using (SqliteConnection connection = new SqliteConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqliteCommand selectCommand = new SqliteCommand($"SELECT COUNT(*) FROM CommentsTable WHERE [CommentsId] = @commentsId;", connection))
                    {
                        selectCommand.Parameters.AddWithValue("@commentsId", item.Id);
                        if ((long)await selectCommand.ExecuteScalarAsync() > 0)
                        {
                            using (SqliteCommand updateCommand = new SqliteCommand($"UPDATE CommentsTable SET [Comments] = @content WHERE [CommentsId] = @commentsId;", connection))
                            {
                                updateCommand.Parameters.AddWithValue("@commentsId", item.Id);
                                updateCommand.Parameters.AddWithValue("@content", item.Content);
                                await updateCommand.ExecuteNonQueryAsync();
                            }
                        }
                        else
                        {
                            using (SqliteCommand insertCommand = new SqliteCommand($"INSERT INTO CommentsTable VALUES(@commentsId, @content);", connection))
                            {
                                insertCommand.Parameters.AddWithValue("@commentsId", item.Id);
                                insertCommand.Parameters.AddWithValue("@content", item.Content);
                                await insertCommand.ExecuteNonQueryAsync();
                            }
                        }
                    }
                    connection.Close();
                    return true;
                }
            }
            catch
            {
                // TODO: log exception
                return false;
            }
        }

        public async Task<bool> DeleteAsync(IComments item)
        {
            try
            {
                using (SqliteConnection connection = new SqliteConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqliteCommand cmd = new SqliteCommand($"DELETE FROM CommentsTable WHERE [CommentsId] = @commentsId;", connection))
                    {
                        cmd.Parameters.AddWithValue("@commentsId", item.Id);
                        await cmd.ExecuteNonQueryAsync();
                    }
                    connection.Close();
                    return true;
                }
            }
            catch
            {
                // TODO: log exception
                return false;
            }
        }

        private async Task InitializeRepository()
        {
            try
            {
                using (SqliteConnection connection = new SqliteConnection(ConnectionString))
                {
                    connection.Open();
                    string createTableQuery = $"CREATE TABLE IF NOT EXISTS CommentsTable ([CommentsId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, [Comments] NVARCHAR({Lengths.CommentsMaxLength}) NULL)";
                    using (SqliteCommand cmd = new SqliteCommand(createTableQuery, connection)) { await cmd.ExecuteReaderAsync(); }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public CommentsRepository()
        {
            DbFileName = "PersonalComments.db"; // TODO: set this value at app's settings.

            string localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string sqlitePath = Path.Combine(localPath, DbFileName);

            ConnectionString = new SqliteConnectionStringBuilder() { DataSource = sqlitePath, Mode = SqliteOpenMode.ReadWriteCreate, Cache = SqliteCacheMode.Default }.ToString();
            InitializeRepository().FireAndForget(false);
        }

        private readonly string ConnectionString;
        private readonly string DbFileName;
    }
}
