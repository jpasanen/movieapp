using MediatR;

namespace movieapp_common.Models;

public class MovieRequestModel : IRequest<MovieModel>
{
    private int _id;

    public MovieRequestModel(int id)
    {
        _id = id;
    }

    public int Id
    {
        get { return _id; }  
        set { _id = value; }
    }
}
