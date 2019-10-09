import React from 'react';
import { Container, Navbar, NavbarBrand } from 'reactstrap';
import './NavMenu.css';
import * as Constants from '../constants';

class NavMenu extends React.Component {

    constructor(props) {
        super(props);
        this.state = { connectionString: '' };
        this.setConnectionString = this.setConnectionString.bind(this);
    }

    componentDidMount() {
        fetch(`${Constants.SERVICEURL}/userdata/connectionstring/`, { credentials: 'include', mode: 'cors' })
            .then(response => response.text())
            .then(data => {
                if (!data.includes("code")) {
                    this.setState({ connectionString: data });
                }
            })
            .catch(error => alert(error));
    }

    setConnectionString(connectionString) {
        fetch(`${Constants.SERVICEURL}/userdata/connectionstring/`, {
                method: 'POST',
                mode: 'cors',
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                credentials: 'include',
                body: encodeURI(`connectionString=${connectionString}`),
            })
            .then(() => {
                this.props.loadCatalogs();
            })
            .catch(error => alert(error));
    }

    render() {
        return (
            <header>
                <Navbar className="navbar-expand-sm navbar-toggleable-sm border-bottom box-shadow mb-3" light>
                    <Container>
                        <NavbarBrand to="/">dbmanager.UI</NavbarBrand>
                        <div className="form-inline mt-2 mt-md-0">
                            <input className="form-control mr-sm-2" type="text" placeholder="db connection string"
                                value={this.state.connectionString}
                                onChange={(event) => this.setState({ connectionString: event.target.value })}
                                title="For example, 'Data Source=localhost;Initial Catalog=master;User Id=sa;Password=P@ssword'"/>
                            <button className="btn btn-outline-success my-2 my-sm-0"
                                onClick={() => this.setConnectionString(this.state.connectionString)} >
                                Load data
                            </button>
                        </div>
                    </Container>
                </Navbar>
            </header>
        );
    }
}

export default NavMenu;
