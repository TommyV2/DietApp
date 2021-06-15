import React from "react";
import { connect } from "react-redux";
import { NavigationBar } from "../../components/NavigationBar"
import { ProfileData } from "../../components/ProfileData"
import { url } from "../../commons/properties"
import { Container, Row, CardGroup, Card, Button, Alert, Col } from "react-bootstrap";
import { DietPlanPopUp } from "./DietPlanPopUp";
import { Link } from "react-router-dom";

function Zalecenie(props) {
    return <div>
        <Card style={{ width: props.width }}>
            <Card.Body>
                <Card.Title><h5>{props.title}</h5></Card.Title>
                <Card.Text>{props.recommendation}</Card.Text>
            </Card.Body>
        </Card>
    </div>

}

export class ClientCollaboration extends React.Component {
    constructor(props) {
        super(props);
        this.state =
        {
            seen: false,
            seen2: false,
            seen3: false,
            refresh: false,
            recommendation: null,
            dietPlan: null,
            dietPlanId: null
        };
        this.handler = this.handler.bind(this);
    }

    togglePop = (dietPlan) => {
        this.setState({
            seen: !this.state.seen,
            dietPlan: dietPlan,
            refresh: true
        });
    }

    handler() {
        this.setState({
            refresh: true
        })
    }

    async componentDidMount() {
        const id = this.props.match.params.id;
        console.log(this.props)
        const response1 = await fetch(url + '/collaborations/' + id);
        const collab = await response1.json();
        const userId = collab.specialistId
        const response2 = await fetch(url + '/specialists/' + userId);
        const userData = await response2.json();
        const response3 = await fetch(url + '/collaborations/' + id + '/recommendations');
        const recommendations = await response3.json();
        const response4 = await fetch(url + '/dietplan?colaboration_id=' + id);
        const dietPlans = await response4.json();

        this.props.dispatch({ type: "LOAD_USER_DATA", userData: userData });
        this.props.dispatch({ type: "LOAD_USER_RECOMMENDATIONS", recommendations: recommendations });
        this.props.dispatch({ type: "LOAD_USER_DIET_PLANS", dietPlans: dietPlans });
    }

    async componentDidUpdate() {
        if (this.state.refresh === true) {
            const id = this.props.match.params.id;

            const response1 = await fetch(url + '/collaborations/' + id);
            const collab = await response1.json();
            const userId = collab.specialistId
            const response2 = await fetch(url + '/specialists/' + userId);
            const userData = await response2.json();
            const response3 = await fetch(url + '/collaborations/' + id + '/recommendations');
            const recommendations = await response3.json();
            const response4 = await fetch(url + '/dietplan?colaboration_id=' + id);
            const dietPlans = await response4.json();

            this.props.dispatch({ type: "LOAD_USER_DATA", userData: userData });
            this.props.dispatch({ type: "LOAD_USER_RECOMMENDATIONS", recommendations: recommendations });
            this.props.dispatch({ type: "LOAD_USER_DIET_PLANS", dietPlans: dietPlans });
            this.setState({
                refresh: false
            })
        }
    }

    render() {

        return (
            <div>
                <NavigationBar type="client" />
                <div>
                    <Container>
                        {!this.props.userData ? (
                            <h3>loading...</h3>
                        ) : (
                            <div>
                                <Alert variant="success "><h3><b>Współpraca z: {this.props.userData.name}</b></h3></Alert>
                                <ProfileData type="specialist" data={this.props.userData} />
                            </div>
                        )}
                    </Container>
                </div>
                <Container className='mt-5' />
                <Container>
                    <Alert variant="success "><h3><b>Zalecenia</b></h3></Alert>
                </Container>
                <div>
                    <Container fluid>
                        <Row className="justify-content-md-center">
                            <CardGroup>
                                {!this.props.recommendations ? (
                                    <h3>loading...</h3>
                                ) : (
                                    this.props.recommendations.map((recommendation, idx) => {
                                        // return <div><label key = {idx} className="wrapper">{recommendation.text}</label><br /></div>
                                        return (
                                            <div>
                                                <Zalecenie width="15rem" title={"Zalecenie " + idx} recommendation={recommendation.text} />
                                            </div>
                                        );
                                    }))}
                            </CardGroup>
                        </Row>
                    </Container>
                </div>
                <Container className='mt-5' />
                <Container>
                    <Alert variant="success "><h3><b>Plany dietetyczne</b></h3></Alert>
                </Container>
                <div>
                    <Container fluid="md">
                        {!this.props.dietPlans ? (
                            <h3>loading...</h3>
                        ) : (
                            this.props.dietPlans.map((dietPlan, idx) => {
                                return (
                                    <Row className="justify-content-md-center">
                                        <Col xs lg="8">
                                            <Alert key={idx} variant="primary">
                                                <p>Plan dietetyczny {dietPlan}</p>
                                                <div className="d-flex justify-content-end">
                                                    <Link to={"/client/collaboration/" + this.props.match.params.id + "/diet_plan/" + dietPlan} class="btn btn-primary">Wyświetl</Link>

                                                </div>
                                            </Alert>
                                        </Col>
                                    </Row>
                                )
                            }))}
                    </Container>
                </div>

                {this.state.seen ? (<DietPlanPopUp collaborationId={this.props.match.params.id} dietPlanId={this.state.dietPlan}
                    dietPlan={this.state.dietPlan} toggle={this.togglePop} handler={this.handler} />) : (null)}
            </div>
        );
    }
};

let mapStateToProps = (state) => {
    return {
        userData: state.userData,
        recommendations: state.recommendations,
        dietPlans: state.dietPlans
    };
};

ClientCollaboration = connect(mapStateToProps)(ClientCollaboration);
