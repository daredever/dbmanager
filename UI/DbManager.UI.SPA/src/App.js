import React from 'react';
import {Container} from 'reactstrap';
import NavMenu from './components/NavMenu';
import Catalogs from './components/Catalogs';
import Tables from './components/Tables';
import Columns from './components/Columns';
import './App.css';
import * as Constants from './constants';

class App extends React.Component {

    constructor(props) {
        super(props);
        this.state = {catalogs: [], tables: [], columns: []};
        this.loadCatalogs = this.loadCatalogs.bind(this);
        this.loadTables = this.loadTables.bind(this);
        this.loadColumns = this.loadColumns.bind(this);
    }

    loadCatalogs() {
        fetch(`${Constants.SERVICEURL}/databaseinfo/catalogs`, {credentials: 'include', mode: 'cors'})
            .then(response => response.json())
            .then(data => {
                if (data.code) {
                    throw Error(data.message);
                }

                this.setState({catalogs: data, tables: [], columns: []});
            })
            .catch(error => alert(error));
    }

    loadTables(catalog) {
        fetch(`${Constants.SERVICEURL}/databaseinfo/tables?name=${catalog.name}`, {
            credentials: 'include',
            mode: 'cors'
        })
            .then(response => response.json())
            .then(data => {
                if (data.code) {
                    throw Error(data.message);
                }

                this.setState({...this.state, tables: data, columns: []});
            })
            .catch(error => alert(error));
    }

    loadColumns(table) {
        fetch(`${Constants.SERVICEURL}/databaseinfo/columns?catalog=${table.catalog}&schema=${table.schema}&name=${table.name}`, {
            credentials: 'include',
            mode: 'cors'
        })
            .then(response => response.json())
            .then(data => {
                if (data.code) {
                    throw Error(data.message);
                }

                this.setState({...this.state, columns: data});
            })
            .catch(error => alert(error));
    }

    render() {
        return (
            <div className="App">
                <NavMenu loadCatalogs={this.loadCatalogs}/>
                <Container>
                    <div className="row">
                        <div className="col">
                            <Catalogs catalogs={this.state.catalogs} loadTables={this.loadTables}/>
                        </div>
                        <div className="col">
                            <Tables tables={this.state.tables} loadColumns={this.loadColumns}/>
                        </div>
                        <div className="col">
                            <Columns columns={this.state.columns}/>
                        </div>
                    </div>
                </Container>
            </div>
        )
    }
}

export default App;
