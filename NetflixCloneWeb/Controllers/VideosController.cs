using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetflixCloneWeb.Data;
using NetflixCloneWeb.Dtos;
using NetflixCloneWeb.Models;
using NetflixCloneWeb.Repositories;

namespace NetflixCloneWeb.Controllers
{
    public class VideosController : Controller
    {
        private readonly DataContext _context;
        private readonly IVideoRepository _videoRepository;
        private readonly IUserRepository _userRepository;

        public VideosController(DataContext context, IVideoRepository videoRepository, IUserRepository userRepository)
        {
            _context = context;
            _videoRepository = videoRepository;
            _userRepository = userRepository;
        }

        // GET: Videos
        public async Task<IActionResult> Index(string? searchString)
        {
            return View(_videoRepository.GetAllVideos(searchString));
        }

        // GET: Videos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var video = _videoRepository.GetVideo(id);

            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        // GET: Videos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Videos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Director,Genre,Type,Rating")] VideoDto videoDto, [Bind("ContentVideo")] IFormFile FileVideo, [Bind("ThumbVideo")] IFormFile FileImage)
        {
            var video = _videoRepository.CreatePostVideo(videoDto, FileImage, FileVideo);
            if (video == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(video);
        }

        // GET: Videos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = _videoRepository.GetVideo(id);

            if (video == null)
            {
                return NotFound();
            }
            return View(video);
        }

        // POST: Videos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Director,Genre,Type,Rating")] VideoDto videoDto, [Bind("ContentVideo")] IFormFile FileVideo, [Bind("ThumbVideo")] IFormFile FileImage)
        {
            var video = _videoRepository.GetVideo(id);
            if (id != video.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    video = _videoRepository.UpdateVideo(id, videoDto, FileImage, FileVideo);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideoExists(video.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(video);
        }

        // GET: Videos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = _videoRepository.GetVideo(id);

            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        // POST: Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var video = _videoRepository.DeleteVideo(id);
            if (!video)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        private bool VideoExists(int id)
        {
            return _context.Vídeos.Any(e => e.Id == id);
        }
    }
}
