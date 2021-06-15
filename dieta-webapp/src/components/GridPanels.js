import React from "react";
// import "./GridPanels.css";
// import { Tile } from "./Tile";
import {Container, Row, CardGroup} from "react-bootstrap";
import {CustomCard} from "./CustomCard";

export class GridPanels extends React.Component {
    render() {
        if (this.props.type === "specialist") {
            return (
                <Container fluid>
                    <Row className="justify-content-md-center">
                        <CardGroup>
                            <CustomCard title='Plany dietetyczne' to='/specialist/diet_plans' width='25rem' src='/images/plany_dietetyczne.jpg'/>
                            <CustomCard title='Plany treningowe' to='/specialist/training_plans' width='25rem' src='/images/workout_plan.jpg'/>
                            <CustomCard title='Produkty i przepisy' to='/specialist/meals' width='25rem' src='/images/meals.jpg'/>
                            <CustomCard title='Ćwiczenia' to='/specialist/excersises' width='25rem' src='/images/exercises.jpg'/>
                        </CardGroup>
                    </Row>
                </Container>
            );
        } else if (this.props.type === "client") {
            return (
                <Container fluid>
                    <Row className="justify-content-md-center">
                        <CardGroup>
                            <CustomCard title='Moja dieta' to='/client/diet' width='25rem' src='/images/meals.jpg'/>
                            <CustomCard title='Mój trening' to='/client/training' width='25rem' src='/images/exercises.jpg'/>
                        </CardGroup>
                    </Row>
                </Container>
            );
        } else {
            return (
                <></>
            );
        }
    }
}