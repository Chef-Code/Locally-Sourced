using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LocallySourced.Models;

namespace LocallySourced.DAL
{
    public class FakeForumRepository : IForumRepository
    {
        private List<Forum> fora;
        int maxID = 0;

        public FakeForumRepository()
        {
            fora = new List<Forum>();
        }

        public FakeForumRepository(List<Forum> f)
        {
            fora = f;
        }
 
        public Forum AddForum(Forum forum)
        {
            forum.ForumID = ++maxID;
            fora.Add(forum);
            return forum;
        }

        public Forum DeleteForumById(int id)
        {
            Forum forum = GetForumByID(id);
            fora.Remove(forum);
            
            return forum;
        }

        public void Dispose()
        {
            //IGNORE
            //this method is provided through/by real repo.
            //Must have this although to satisfy contract with interface
        }

        public List<Forum> GetAllForums()
        {
            return fora;
        }

        public Forum GetForumByID(int? id)
        {
            return fora.Find(f => f.ForumID == id);
        }

        public int UpdateForum(Forum forum)
        {
            int forumUpdated = 0;
            if(DeleteForumById(forum.ForumID) != null)
            {
                fora.Add(forum);
                forumUpdated = 1;
            }
            return forumUpdated;
        }
    }
}