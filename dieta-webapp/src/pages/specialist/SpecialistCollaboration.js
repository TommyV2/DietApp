import React from "react";
import { connect } from "react-redux";
import { NavigationBar } from "../../components/NavigationBar"
import { ProfileData } from "../../components/ProfileData"
import { url } from "../../commons/properties"
import { getCookie } from "../../commons/getCookie";
import { Button, Row, Col, Container, Alert } from "react-bootstrap";
import { AddRecommendationPopUp } from "./AddRecommendationPopUp";
import { AddDietPlanPopUp } from "./AddDietPlanPopUp";
// import { ModifyDietPlanPopUp } from "./ModifyDietPlanPopUp";
import { Link } from "react-router-dom";


export class SpecialistCollaboration extends React.Component {

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
            specialistId: getCookie("specialistId")
        };
        this.handler = this.handler.bind(this);
    }


    togglePop = (recommendation) => {
        this.setState({
            seen: !this.state.seen,
            recommendation: recommendation,
            refresh: true
        });
    }

    togglePopAddDietPlan = (dietPlan) => {
        this.setState({
            seen2: !this.state.seen2,
            dietPlan: dietPlan,
            refresh: true
        });
    }

    togglePopModifyDietPlan = (dietPlan) => {
        this.setState({
            seen3: !this.state.seen3,
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

        const response1 = await fetch(url + '/collaborations/' + id);
        const collab = await response1.json();
        const userId = collab.customerId
        const response2 = await fetch(url + '/customers/' + userId);
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
            const userId = collab.customerId
            const response2 = await fetch(url + '/customers/' + userId);
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
                <NavigationBar type="specialist" />
                <div>
                    <Container>
                        {!this.props.userData ? (
                            <h3>loading...</h3>
                        ) : (
                            <div>
                                <Alert variant="success "><h3><b>Współpraca z: {this.props.userData.name}</b></h3></Alert>
                                <ProfileData type="customer" data={this.props.userData} />
                            </div>
                        )}
                    </Container>
                </div>
                <Container className='mt-5' />
                <Container>
                    <Alert variant="success "><h3><b>Zalecenia</b></h3></Alert>
                </Container>
                <div>
                    <Container fluid="md">
                        {!this.props.recommendations ? (
                            <h3>loading...</h3>
                        ) : (
                            this.props.recommendations.map((recommendation, idx) => {
                                return (
                                    <Row className="justify-content-md-center">
                                        <Col xs lg="8">
                                            <Alert variant="info">
                                                {recommendation.text}
                                                <div className="d-flex justify-content-end">
                                                    <Button size="sm" onClick={() => this.togglePop(recommendation)}>Edytuj</Button>
                                                </div>
                                            </Alert>
                                        </Col>
                                    </Row>
                                )
                            }))}
                        <Row className="justify-content-md-center">
                            <Col xs lg="8">
                                <Alert variant="info">
                                    <div className="d-flex justify-content-end">
                                        <Button size="sm" onClick={() => this.togglePop(null)}>Dodaj zalecenie</Button>
                                    </div>
                                </Alert>
                            </Col>
                        </Row>
                    </Container>
                </div>
                {this.state.seen ? <AddRecommendationPopUp collaborationId={this.props.match.params.id} ownerId="1" recommendationId={this.state.recommendation ? (this.state.recommendation.id) : null}
                    recommendation={this.state.recommendation} toggle={this.togglePop} handler={this.handler} /> : null}
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
                                                    {/* <Button size="sm" onClick={() => this.togglePopModifyDietPlan(dietPlan)}>Wyświetl</Button> */}
                                                    <Link to={"/specialist/collaboration/"+this.props.match.params.id+"/diet_plan/"+dietPlan} class="btn btn-primary">Wyświetl</Link>
                                                </div>
                                            </Alert>
                                        </Col>
                                    </Row>
                                )
                            }))}
                        <Row className="justify-content-md-center">
                            <Col xs lg="8">
                                <Alert variant="primary">
                                    <div className="d-flex justify-content-end">
                                        <Button size="sm" onClick={() => this.togglePopAddDietPlan(null)}>Dodaj plan dietetyczny</Button>                                                </div>
                                </Alert>
                            </Col>
                        </Row>
                    </Container>
                </div>
                {this.state.seen2 ? (<AddDietPlanPopUp collaborationId={this.props.match.params.id} ownerId="1" dietPlanId={null}
                    dietPlan={this.state.dietPlan} toggle={this.togglePopAddDietPlan} handler={this.handler} />) : (null)}
{/* 
                {this.state.seen3 ? (<ModifyDietPlanPopUp collaborationId={this.props.match.params.id} ownerId="1" dietPlanId={this.state.dietPlan}
                    dietPlan={this.state.dietPlan} toggle={this.togglePopModifyDietPlan} handler={this.handler} />) : (null)} */}

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

SpecialistCollaboration = connect(mapStateToProps)(SpecialistCollaboration);
