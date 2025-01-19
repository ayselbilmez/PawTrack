using Microsoft.EntityFrameworkCore;
using PawTrack.Business.Operations.Comment.Dtos;
using PawTrack.Business.Types;
using PawTrack.Data.Entities;
using PawTrack.Data.Repositories;
using PawTrack.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawTrack.Business.Operations.Comment
{
    public class CommentManager : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<CommentEntity> _commentRepository;

        public CommentManager(IUnitOfWork unitOfWork, IRepository<CommentEntity> commentRepository)
        {
            _commentRepository = commentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceMessage> AddComment(AddCommentDto comment)
        {
            var commentEntity = new CommentEntity
            {
                UserId = comment.UserId,
                AppointmentId = comment.AppointmentId,
                Content = comment.Content
            };

            _commentRepository.Add(commentEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Yorum kaydedilemedi");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Bsaariyla yorumunuz kaydedildi."
            };            
        }

        public async Task<ServiceMessage> Delete(int id)
        {
            var comment = _commentRepository.GetById(id);

            if (comment == null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Boyle bir yorum bulunamadi"
                };
            }

            _commentRepository.Delete(comment);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Yorum silinemedi");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Basari ile silindi"
            };
        }

        public async Task<List<CommentDto>> GetComments(int appointmentId)
        {
            var comments = await _commentRepository.GetAll(x => x.AppointmentId == appointmentId)
                .Select(c => new CommentDto
                {
                    Id = c.Id,
                    UserId = c.UserId,
                    AppointmentId= c.AppointmentId,
                    Content = c.Content
                }).ToListAsync();

            return comments;
        }
    }
}
