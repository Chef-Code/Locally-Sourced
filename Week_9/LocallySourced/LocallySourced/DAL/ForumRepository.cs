using LocallySourced.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LocallySourced.DAL
{
    public class ForumRepository : IDisposable, IForumRepository
    {
        LocallySourcedDB context = new LocallySourcedDB();

        public List<Forum> GetAllForums()
        {
            return context.Fora.ToList();
        }

        public Forum GetForumByID(int? id)
        {
            return context.Fora.Find(id);
        }
        //public void AddForum(LocallySourcedDB ctx, Forum forum)
        //{
        //        Forum ctxForum = context.Fora.AddOrUpdate(forum);
        //        context.SaveChanges();
        //}

        public Forum AddForum(Forum forum)
        {
            Forum ctxForum = context.Fora.Add(forum);
            context.SaveChanges();

            return ctxForum;
        }
        
        public int UpdateForum(Forum forum)
        {
            context.Entry(forum).State = EntityState.Modified;
            var myInt = context.SaveChanges();
            return myInt;
        }

        public Forum DeleteForumById(int id)
        {
            Forum forum = GetForumByID(id);
            context.Fora.Remove(forum);
            context.SaveChanges();
            return forum;
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}