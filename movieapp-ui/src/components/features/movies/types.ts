interface MovieGenre {
    id: number;
    name: string;
}

export interface PopularMovieDto {
    id: number;
    original_language: string;
    original_title: string;
    overview: string;
    poster_path: string;
    release_date: string;
    title: string;
}

export interface FullMovieDto extends PopularMovieDto {
    adult: boolean;
    homepage: string;
    imdb_id: string;
    status: string;
    time: string;
    date: string;
    theater: number;
    genres: MovieGenre[]
}