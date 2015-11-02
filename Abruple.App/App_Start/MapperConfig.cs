using Abruple.App.Models.BindingModels.Contest;

namespace Abruple.App.App_Start
{
    using System.Linq;
    using Abruple.Models;
    using Abruple.Models.Enums;
    using AutoMapper;
    using Models.ViewModels;
    using Models.ViewModels.Contest;
    using Models.ViewModels.ContestEntry;
    using Models.ViewModels.User;

    public class MapperConfig
    {
        public static void ConfigureMappings()
        {
            Mapper.CreateMap<Contest, ContestConciseViewModel>()
                .ForMember(model => model.Author, config => config.MapFrom(contest => contest.Creator.UserName))
                .ForMember(model => model.Date, config => config.MapFrom(contest => contest.CreatedOn))
                .ForMember(model => model.EntriesCount, config => config.MapFrom(contest => contest.ContestEntries.Count));

            Mapper.CreateMap<ContestEntry, ContestEntryConciseViewModel>()
                .ForMember(model => model.Author, config => config.MapFrom(contestEntry => contestEntry.Author.UserName))
                .ForMember(model => model.Contest, config => config.MapFrom(contestEntry => contestEntry.Contest.Title))
                .ForMember(model => model.Votes, config => config.MapFrom(contestEntry => contestEntry.Votes.Count));

            Mapper.CreateMap<ContestEntry, ContestEntryShortViewModel>()
               .ForMember(model => model.Author, config => config.MapFrom(contestEntry => contestEntry.Author.UserName))
               .ForMember(model => model.Contest, config => config.MapFrom(contestEntry => contestEntry.Contest.Title))
               .ForMember(model => model.Votes, config => config.MapFrom(contestEntry => contestEntry.Votes.Count));

            Mapper.CreateMap<Vote, VoteViewModel>()
                .ForMember(model => model.Autor, config => config.MapFrom(vote => vote.Author.UserName));

            Mapper.CreateMap<Notification, NotificationViewModel>()
                .ForMember(model => model.Recipient,
                    config => config.MapFrom(notification => notification.Recipient.UserName));

            Mapper.CreateMap<User, UserPersonalDataViewModel>()
                .ForMember(model => model.ContestsParticipatedCount,
                    config => config.MapFrom(c => c.ContestsParticipated.Count))
                .ForMember(model => model.ContestsCreatedCount,
                    config => config.MapFrom(c => c.ContestsCreated.Count))
                .ForMember(model => model.PhotosUpplodedCount,
                    config => config.MapFrom(ce => ce.ContestEntries.Count(cc => cc.State != ContestEntryState.Deleted)))
                .ForMember(model => model.WinningPhotosCount,
                    config =>config.MapFrom(
                            ce =>ce.ContestEntries.Count(
                                    cc => cc.IsWinner == true && cc.State != ContestEntryState.Deleted)));
                       
            //Mapper.CreateMap<User, UserFullViewModel>()
            //    .ForMember(model => model.ContestEntries,
            //        config => config.MapFrom(u => u.ContestEntries.Where(ce => ce.State != ContestEntryState.Deleted).OrderByDescending(ce => ce.Upploaded)))
            //    .ForMember(model => model.ContestsCreated,
            //        config => config.MapFrom(u => u.ContestsCreated.OrderByDescending(c => c.CreatedOn)))
            //    .ForMember(model => model.ContestsParticipated,
            //        config => config.MapFrom(u => u.ContestsParticipated.Where(c => c.State != ContestState.Active).OrderByDescending(c => c.CreatedOn)));

            Mapper.CreateMap<NewContestBindingModel, Contest>();

            //Mapping with inheritance
            //Mapper.CreateMap<ParentSource, ParentDestination>()
            //.Include<ChildSource, ChildDestination>();
        }
    }
}