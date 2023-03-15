import { useQuery } from 'react-query';
import { apiGet } from '@services/api/apiService';
import { FullMovieDto } from './types';

async function getAllMoviesAsync(theaterId: number): Promise<FullMovieDto[]> {
  return await apiGet<any, FullMovieDto[]>(`/theater/${theaterId}`);
}

export function useGetAllMovies(theaterId: number) {
  return useQuery(['movies', theaterId], () => getAllMoviesAsync(theaterId), {
    refetchOnWindowFocus: false,
  });
}
