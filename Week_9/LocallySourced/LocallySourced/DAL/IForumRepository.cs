using System.Collections.Generic;
using LocallySourced.Models;

namespace LocallySourced.DAL
{
    public interface IForumRepository
    {
        Forum AddForum(Forum forum);
        Forum DeleteForumById(int id);
        void Dispose();
        List<Forum> GetAllForums();
        Forum GetForumByID(int? id);
        int UpdateForum(Forum forum);
    }
}