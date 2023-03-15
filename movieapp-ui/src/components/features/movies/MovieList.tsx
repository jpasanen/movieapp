import { useGetAllMovies } from "./useGetAllMovies";
import MovieCard from "./MovieCard";
import { FullMovieDto } from "./types";
import { modalStyle } from "@components/layout/modalStyle";
import { useState } from "react";
import {
  Button,
  Card,
  CardActions,
  CardHeader,
  CardContent,
  CardActionArea,
  Typography,
  Box,
  Modal,
  Skeleton,
} from "@mui/material";

const movie: FullMovieDto = {
  id: 0,
  original_language: "",
  original_title: "",
  overview: "",
  poster_path: "",
  release_date: "",
  title: "",
  adult: false,
  date: "",
  genres: [],
  homepage: "",
  imdb_id: "",
  status: "",
  theater: 0,
  time: "",
};

type MovieListProps = {
  theaterId: number;
};

const MovieList = (props: MovieListProps) => {
  const {
    data: movies = [],
    isLoading,
    isError,
    isRefetching,
    refetch,
  } = useGetAllMovies(props.theaterId);
  const [open, setOpen] = useState(false);
  const [modalData, setData] = useState<FullMovieDto>(movie);

  const handleOpen = (data: FullMovieDto) => {
    setOpen(true);
    setData(data);
  };
  const handleClose = () => setOpen(false);

  async function refetchMovies() {
    await refetch();
  }

  const MovieModal = () => {
    return modalData ? (
      <Modal
        aria-labelledby="simple-modal-title"
        aria-describedby="simple-modal-description"
        open={open}
        onClose={handleClose}
      >
        <Box sx={modalStyle}>
          <Typography variant="h6" id="modal-title">
            {modalData.original_title}
          </Typography>
          <Typography variant="subtitle1" id="simple-modal-description">
            Language: {modalData.original_language}
          </Typography>
          <Typography variant="body2" id="simple-modal-description">
            Released: {modalData.release_date}
          </Typography>
          <Typography variant="body2" id="simple-modal-description">
            Genres:{" "}
            {modalData.genres
              .reduce(
                (previous, current) => (previous += `,${current.name}`),
                ""
              )
              .substring(1)}
          </Typography>
          <img src={modalData.poster_path} />
          <Typography variant="body2" id="simple-modal-description">
            {modalData.overview}
          </Typography>
          <p>
            <Button size="small">Get tickets</Button>
          </p>
        </Box>
      </Modal>
    ) : null;
  };

  return (
    <Box sx={{ flexGrow: 1 }}>
      {movies
        .sort(
          (a, b) =>
            Number(a.time.replaceAll(":", "")) -
            Number(b.time.replaceAll(":", ""))
        )
        .map((elem: FullMovieDto) => (
          <MovieCard
            isLoading={isLoading}
            movie={elem}
            onCardOpen={handleOpen}
          />
        ))}
      <MovieModal />
    </Box>
  );
};

export default MovieList;
