import React from "react";
import { Nav, Navbar } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap"
import { Link } from "react-router-dom";
import "./Navbar.css";

export class NavigationBar extends React.Component {
    render() {
        if (this.props.type === "main") {
            return (
                <div class="navbar">
                    <Navbar bg="light" expand="lg">

                        <LinkContainer to="/">
                            <Navbar.Brand>Projekt Dieta</Navbar.Brand>
                        </LinkContainer>
                        <Navbar.Toggle aria-controls="basic-navbar-nav" />
                        <Navbar.Collapse id="basic-navbar-nav">
                            <Nav className="mr-auto">
                                <LinkContainer to="/about">
                                    <Nav.Link>O projekcie</Nav.Link>
                                </LinkContainer>
                            </Nav>
                        </Navbar.Collapse>
                    </Navbar>
                </div>
            );
        } else if (this.props.type === "specialist") {
            return (
                <div class="navbar">
                    <Navbar bg="light" expand="lg">
                        <LinkContainer to="/specialist/dashboard">
                            <Navbar.Brand className="button">Dashboard</Navbar.Brand>
                        </LinkContainer>

                        <Navbar.Toggle aria-controls="basic-navbar-nav" />
                        <Navbar.Collapse id="basic-navbar-nav">
                            <Nav className="mr-auto">
                                <LinkContainer to="/specialist/profile">
                                    <Nav.Link className="button">Profil </Nav.Link>
                                </LinkContainer>
                                <LinkContainer to="/">
                                    <Nav.Link className="button">Wyloguj się</Nav.Link>
                                </LinkContainer>
                            </Nav>
                        </Navbar.Collapse>
                    </Navbar>
                </div>
            );
        } else if (this.props.type === "client") {
            return (
                <div class="navbar">
                    <Navbar bg="light" expand="lg">
                        <LinkContainer to="/client/dashboard">
                            <Navbar.Brand className="button">Dashboard</Navbar.Brand>
                        </LinkContainer>
                        <Navbar.Toggle aria-controls="basic-navbar-nav" />
                        <Navbar.Collapse id="basic-navbar-nav">
                            <Nav className="mr-auto">
                                <LinkContainer to="/client/profile">
                                    <Nav.Link className="button">Profil</Nav.Link>
                                </LinkContainer>
                                <LinkContainer to="/">
                                    <Nav.Link className="button">Wyloguj się</Nav.Link>
                                </LinkContainer>

                            </Nav>
                        </Navbar.Collapse>
                    </Navbar>
                </div>
            );
        }


    }
}