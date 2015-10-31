namespace Abruple.App.App_Start
{
    using Abruple.Models;
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

            Mapper.CreateMap<Vote, VoteViewModel>()
                .ForMember(model => model.Autor, config => config.MapFrom(vote => vote.Author.UserName));

            Mapper.CreateMap<Notification, NotificationViewModel>()
                .ForMember(model => model.Recipient,
                    config => config.MapFrom(notification => notification.Recipient.UserName));

            //Mapper.CreateMap<User, UserFullViewModel>()
            //    .ForMember(model => model.ContestEntries, config => config.MapFrom<ContestEntry>(), ContestEntryConciseViewModel>())
        }
    }
}