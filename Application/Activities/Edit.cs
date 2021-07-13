using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _contex;
            private readonly IMapper _mapper;
            public Handler(DataContext contex, IMapper mapper)
            {
                _mapper = mapper;
                _contex = contex;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _contex.Activities.FindAsync(request.Activity.Id);

                _mapper.Map(request.Activity, activity);
                
                await _contex.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}