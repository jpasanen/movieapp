import { FullMovieDto } from './types';
import { Button, Card, CardActions, CardHeader, CardContent, 
    CardActionArea, Typography, Skeleton } from '@mui/material';

type MovieCardProps = {
    movie: FullMovieDto;
    isLoading: boolean;
    onCardOpen(movie:FullMovieDto): void;
};    

const MovieCard = (props:MovieCardProps) => {
    return (
        <Card key={props.movie.id} sx={{ maxWidth: 600 }} variant='outlined'>
            <CardActionArea onClick={() => props.onCardOpen(props.movie)}>
                <CardHeader                    
                    title={props.isLoading ? <Skeleton variant="text" width="25%" /> : `${props.movie.time.substring(0, props.movie.time.length - 3)} - ${props.movie.title}`}
                />
                <CardContent>
                    <Typography variant="body2" gutterBottom>
                    {props.isLoading ? <Skeleton variant="text" width="25%" /> : `${props.movie.overview.substring(0, 100)}...`}
                    </Typography>
                    <Typography align='left' variant="body2" color="text.secondary">
                        Theater: {props.movie.theater}
                    </Typography>
                </CardContent>
            </CardActionArea>
            <CardActions>
                <Button size="small">Get tickets</Button>                    
            </CardActions>
        </Card>
    );
};

export default MovieCard