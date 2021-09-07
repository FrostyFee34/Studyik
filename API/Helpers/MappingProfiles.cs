using API.DTO;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<JobDTO, Job>()
                .ForMember(o => o.Id, j => j.MapFrom(s => s.Id))
                .ForMember(o => o.Header, j => j.MapFrom(s => s.Header))
                .ForMember(o => o.Text, j => j.MapFrom(s => s.Text))
                .ForMember(o => o.IsDone, j => j.MapFrom(s => s.IsDone))
                .ForMember(o => o.ReminderTime, j => j.MapFrom(s => s.ReminderTime));

            CreateMap<ArticleDTO, Article>()
                .ForMember(o => o.Id, j => j.MapFrom(s => s.Id))
                .ForMember(o => o.Header, a => a.MapFrom(s => s.Header))
                .ForMember(o => o.Text, a => a.MapFrom(s => s.Text))
                .ForMember(o => o.IsDone, a => a.MapFrom(s => s.IsDone))
                .ForMember(o => o.Link, a => a.MapFrom(s => s.Link))
                .ForMember(o => o.ReminderTime, a => a.MapFrom(s => s.ReminderTime))
                .ForMember(o => o.JobId, a => a.MapFrom(s => s.JobId));

            CreateMap<VideoDTO, Video>()
                .ForMember(o => o.Id, j => j.MapFrom(s => s.Id))
                .ForMember(o => o.Header, v => v.MapFrom(s => s.Header))
                .ForMember(o => o.Text, v => v.MapFrom(s => s.Text))
                .ForMember(o => o.IsDone, v => v.MapFrom(s => s.IsDone))
                .ForMember(o => o.Link, v => v.MapFrom(s => s.Link))
                .ForMember(o => o.ReminderTime, v => v.MapFrom(s => s.ReminderTime))
                .ForMember(o => o.JobId, v => v.MapFrom(s => s.JobId));

            CreateMap<NoteDTO, Note>()
                .ForMember(o => o.Id, j => j.MapFrom(s => s.Id))
                .ForMember(o => o.Header, n => n.MapFrom(s => s.Header))
                .ForMember(o => o.Text, n => n.MapFrom(s => s.Text))
                .ForMember(o => o.ArticleId, n => n.MapFrom(s => s.ArticleId))
                .ForMember(o => o.VideoId, n => n.MapFrom(s => s.VideoId))
                .ForMember(o => o.JobId, n => n.MapFrom(s => s.JobId));


        }
    }
}