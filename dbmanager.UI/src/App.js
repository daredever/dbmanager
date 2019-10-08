import React from 'react';
import { Container } from 'reactstrap';
import NavMenu from './components/NavMenu';
import Catalogs from './components/Catalogs';
import Tables from './components/Tables';
import Columns from './components/Columns';
import './App.css';

const url = 'https://localhost:5001/api';

class App extends React.Component {

    constructor(props) {
        super(props);
        this.state = { catalogs: [], tables: [], columns: [] };
        this.setConnectionString = this.setConnectionString.bind(this);
        this.loadCatalogs = this.loadCatalogs.bind(this);
        this.loadTables = this.loadTables.bind(this);
        this.loadColumns = this.loadColumns.bind(this);
        this.createTable = this.createTable.bind(this);
    }

    setConnectionString(connectionString) {
        fetch(`${url}/userdata/connectionstring/`, {
                method: 'POST',
                mode: 'cors',
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                credentials: 'include',
                body: encodeURI(`connectionString=${connectionString}`),
            })
            .then(() => {
                this.loadCatalogs();
            })
            .catch(error => alert(error));
    }

    loadCatalogs() {
        fetch(`${url}/databaseinfo/catalogs`, { credentials: 'include', mode: 'cors' })
            .then(response => response.json())
            .then(data => {
                if (data.code) {
                    throw Error(data.message);
                }

                this.setState({ catalogs: data, tables: [], columns: [] });
            })
            .catch(error => alert(error.message));
    }

    loadTables(catalog) {
        fetch(`${url}/databaseinfo/tables/${catalog.name}`, { credentials: 'include', mode: 'cors' })
            .then(response => response.json())
            .then(data => {
                if(data.code) {
                    throw Error(data.message);
                }

                this.setState({ ... this.state, tables: data, columns: [] });
            })
            .catch(error => alert(error));
    }

    loadColumns(table) {
        fetch(`${url}/databaseinfo/columns/${table.catalog}/${table.schema}/${table.name}`, { credentials: 'include', mode: 'cors' })
            .then(response => response.json())
            .then(data => {
                if(data.code) {
                    throw Error(data.message);
                }

                this.setState({ ... this.state, columns: data });
            })
            .catch(error => alert(error));
    }

    createTable(table) {
        fetch(`${url}/generatescript/createtable/${table.catalog}/${table.schema}/${table.name}`, { credentials: 'include', mode: 'cors' })
            .then(response => response.text())
            .then(data => {
                alert(data);
            })
            .catch(error => alert(error));
    }

    render() {
        return (
            <div className="App">
                <NavMenu setConnectionString={this.setConnectionString} />
                <Container>
                    <div className="row">
                        <div className="col">
                            <Catalogs catalogs={this.state.catalogs} loadTables={this.loadTables} />
                        </div>
                        <div className="col">
                            <Tables tables={this.state.tables} loadColumns={this.loadColumns} createTable={this.createTable} />
                        </div>
                        <div className="col">
                            <Columns columns={this.state.columns} />
                        </div>
                    </div>
                </Container>
            </div>
        );
    }
}

export default App;
