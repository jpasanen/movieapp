import { FormControl, InputLabel, Box, MenuItem, useThemeProps } from '@mui/material';
import Select, { SelectChangeEvent } from '@mui/material/Select';
import { useState } from 'react';

type FilterProps = {
    onTheaterChange(theaterId: number): void;
    theaterId: number
};

const Filters = (props: FilterProps) => {
    const [theater, setTheater] = useState(0);

    const handleChange = (event: SelectChangeEvent) => {
        props.onTheaterChange(Number(event.target.value));
        setTheater(Number(event.target.value));
    };

    return (
        <Box sx={{ minWidth: 120 }}>
            <FormControl variant="filled" fullWidth>
                <InputLabel id="demo-simple-select-filled-label">Theater</InputLabel>
                <Select
                    labelId="demo-simple-select-filled-label"
                    id="demo-simple-select"
                    value={theater.toString()}
                    label="Theater"
                    onChange={handleChange}
                    >
                    <MenuItem value={'0'}>
                        <em>None</em>
                    </MenuItem>
                    <MenuItem value={'1'}>Kuopio: SCALA</MenuItem>
                    <MenuItem value={'6'}>Helsinki: TENNISPALATSI</MenuItem>
                    <MenuItem value={'13'}>Espoo: SELLO</MenuItem>
                </Select>
            </FormControl>
        </Box>
    )
};

export default Filters;