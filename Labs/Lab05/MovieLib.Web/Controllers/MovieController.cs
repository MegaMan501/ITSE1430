using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieLib.Data.Sql;
using MovieLib.Web.Models;

namespace MovieLib.Web.Controllers
{
    [DescriptionAttribute("Handles Movie Requests")]
    public class MovieController : Controller
    {
        #region Construction
        public MovieController() : this(GetDatabase())
        {
        }

        public MovieController( IMovieDatabase database )
        {
            _database = database;
        }

        #endregion

        public ActionResult Add ()
        {
            var model = new MovieViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add( MovieViewModel model )
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _database.Add(model.ToDomain());

                    return RedirectToAction("List");
                } catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                };
            };

            return View(model);
        }
        public ActionResult Edit( int id )
        {
            var existing = _database.Get(id);
            if (existing == null)
                return HttpNotFound();
            
            return View(existing.ToModel());
        }

        [HttpPost]
        public ActionResult Edit( MovieViewModel model )
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _database.Update(model.ToDomain());

                    return RedirectToAction("List");
                } catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                };
            };

            return View(model);
        }

        public ActionResult Delete( int id )
        {
            var existing = _database.Get(id);
            if (existing == null)
                return HttpNotFound();

            return View(existing.ToModel());
        }

        [HttpPost]
        public ActionResult Delete ( MovieViewModel model )
        {
            try
            {
                _database.Remove(model.Id);

                return RedirectToAction("List");
            } catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            };

            return View(model);
        }

        // GET: Movie
        public ActionResult List()
        {
            var movies = _database.GetAll();

            return View(movies.ToModel());
        }

        #region Private Members
        private static IMovieDatabase GetDatabase()
        {
            var connString = ConfigurationManager.ConnectionStrings["MovieDatabase"];

            return new SqlMovieDatabase(connString.ConnectionString);
        }

        private readonly IMovieDatabase _database;
        #endregion

    }
}