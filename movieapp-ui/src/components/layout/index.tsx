import Header from './Header'
import MovieList from '../features/movies/MovieList';
import Filters from '../features/movies/Filters';
import { SelectChangeEvent } from '@mui/material/Select';
import { useState } from 'react';

const Layout = () => {
    const [theater, setTheater] = useState(0);

    const handleTheaterChange = (theaterId: number) => {
        setTheater(theaterId);
    }

    return (
        <div style={{width: 600}}>
            <Header />
            <Filters onTheaterChange={handleTheaterChange} theaterId={theater} />
            {theater > 0 &&
            <MovieList theaterId={theater} />}
        </div>
    )
};

export default Layout;