import React from 'react';
import { Container, Navbar, NavbarBrand } from 'reactstrap';
import './NavMenu.css';

function NavMenu() {
    return (
        <header>
            <Navbar className="navbar-expand-sm navbar-toggleable-sm border-bottom box-shadow mb-3" light>
                <Container>
                    <NavbarBrand to="/">dbmanager.UI</NavbarBrand>
                    <div className="form-inline mt-2 mt-md-0">
                        <input className="form-control mr-sm-2" type="text" placeholder="db connection string" />
                        <button className="btn btn-outline-success my-2 my-sm-0">
                            Load data
                            </button>
                    </div>
                </Container>
            </Navbar>
        </header>
    );
}

export default NavMenu;
