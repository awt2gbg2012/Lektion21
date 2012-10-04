using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQLInjection.Models
{
    public class CommentRepo
    {
        private static CommentRepo _instance;
        public static CommentRepo Instance
        {
            get
            {
                if (null == _instance)
                    _instance = new CommentRepo();
                return _instance;
            }
        }
        private List<string> _comments;
        private CommentRepo()
        {
            _comments = new List<string> { "Please keep it civil.", 
                "Everything I wrote is backed up by solid proof. Admins are on a power trip. Lizardmen run the government. How can you not see this?!?!!",
                "Get a life you miserable swamp dweller!" };
        }

        public List<string> FindAll() { return _comments; }
        public void Save(string comment) { _comments.Add(comment); }

    }
}