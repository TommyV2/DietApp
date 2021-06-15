import React from "react";
import { connect } from "react-redux";
import { NavigationBar } from "../../components/NavigationBar"
import { GridPanels } from "../../components/GridPanels"
import { Collaborations } from "../../components/Collaborations"
import { Invitations } from "../../components/Invitations"
import { UserList } from "../../components/UserList"
import { Row, Col, Alert } from "react-bootstrap";
import { url } from "../../commons/properties";
import { Container } from "react-bootstrap";
import { getCookie } from "../../commons/getCookie";

export class SpecialistDashboard extends React.Component {
    constructor(props) {
        super(props);
        this.state =
        {
            refresh: false,
            r: 0,
            specialistId: getCookie("specialistId")
        };

        this.handler = this.handler.bind(this);
    }

    async componentDidMount() {
        console.log(this.state.specialistId);
        const response = await fetch(url + '/specialists/' + this.state.specialistId);
        const response2 = await fetch(url + '/specialists/' + this.state.specialistId + '/collaborations');
        const response3 = await fetch(url + '/specialists/' + this.state.specialistId + '/invitations');
        const userData = await response.json();
        const userCollaborations = await response2.json();
        const userInvitations = await response3.json();
        this.props.dispatch({ type: "LOAD_USER_DATA", userData: userData });
        this.props.dispatch({ type: "LOAD_USER_COLLABORATIONS", userCollaborations: userCollaborations });
        this.props.dispatch({ type: "LOAD_USER_INVITATIONS", userInvitations: userInvitations });
    }

    async componentDidUpdate() {
        console.log(this.state.r + " " + this.state.refresh);
        if (this.state.refresh === true) {
            const response = await fetch(url + '/specialists/' + this.state.specialistId);
            const response2 = await fetch(url + '/specialists/' + this.state.specialistId + '/collaborations');
            const response3 = await fetch(url + '/specialists/' + this.state.specialistId + '/invitations');
            const userData = await response.json();
            const userCollaborations = await response2.json();
            const userInvitations = await response3.json();
            this.props.dispatch({ type: "LOAD_USER_DATA", userData: userData });
            this.props.dispatch({ type: "LOAD_USER_COLLABORATIONS", userCollaborations: userCollaborations });
            this.props.dispatch({ type: "LOAD_USER_INVITATIONS", userInvitations: userInvitations });
            // let val = this.state.prev;   
            this.setState({
                refresh: false
            })
        }
    }

    handler() {
        let val = this.state.r;
        this.setState({
            r: val + 1,
            refresh: true
        })
    }

    render() {
        const handler = this.handler;
        const r = this.state.r;
        return (
            <div>
                <NavigationBar type="specialist" />
                <Container className='mt-3' />
                {!this.props.userData ? (
                    <h3>loading...</h3>
                ) : (
                    <Container>
                        <Row className="justify-content-md-center">
                            <Col xs lg="8">
                                <Alert variant="success"><center><h3><b>Witaj, {this.props.userData.name}!</b></h3></center></Alert>
                            </Col>
                            
                        </Row>
                    </Container>
                )}
                <Container className='mt-3' />
                <GridPanels type="specialist" />
                <Container className='mt-5'>
                    <Row>
                        <Col>
                            <Container>
                                <Alert variant="success"><h3><b>Podjęte współprace</b></h3></Alert>
                                {!this.props.userCollaborations ? (
                                    <h3>loading...</h3>
                                ) : (
                                    <Collaborations r={r} type="specialist" handler={handler}/*collaborations={this.props.userCollaborations}*/ />
                                )}
                            </Container>
                        </Col>
                        <Col>
                            <Container>
                                <Alert variant="success"><h3><b>Zaproszenia do współpracy</b></h3></Alert>
                                {!this.props.userInvitations ? (
                                    <h3>loading...</h3>
                                ) : (
                                    <Invitations r={r} type="specialist" handler={handler}/*collaborations={this.props.userCollaborations}*/ />
                                )}
                            </Container>
                        </Col>
                        <Col>
                            <Container>
                                <Alert variant="success"><h3><b>Wyszukaj i zaproś klienta</b></h3></Alert>
                                {!this.props.userInvitations ? (
                                    <h3>loading...</h3>
                                ) : (
                                    <UserList r={r} type="specialist" handler={handler} /*collaborations={this.props.userCollaborations}*/ />
                                )}
                            </Container>
                        </Col>
                    </Row>
                </Container>
            </div>
        )
    }
};

const mapStateToProps = (state) => {
    return {
        userData: state.userData,
        userCollaborations: state.userCollaborations,
        userInvitations: state.userInvitations
    };
};

SpecialistDashboard = connect(mapStateToProps)(SpecialistDashboard);
