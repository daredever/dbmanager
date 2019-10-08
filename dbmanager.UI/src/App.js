import React from 'react';
import { Container } from 'reactstrap';
import NavMenu from './components/NavMenu';
import Catalogs from './components/Catalogs';
import Tables from './components/Tables';
import Columns from './components/Columns';
import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';

function App() {
    return (
        <div className="App">
            <NavMenu />
            <Container>
                <div className="row">
                    <div className="col">
                        <Catalogs />
                    </div>
                    <div className="col">
                        <Tables />
                    </div>
                    <div className="col">
                        <Columns />
                    </div>
                </div>
            </Container>
        </div>
    );
}

export default App;
