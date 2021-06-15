import React from "react";
import { Container, InputGroup, FormControl } from 'react-bootstrap';
import "./ProfileData.css";

class DataLine extends React.Component {
    render() {
        return (
            <InputGroup className="mb-1">
            <InputGroup.Prepend >
                <InputGroup.Text id="inputGroup-sizing-sm">{this.props.title}</InputGroup.Text>
            </InputGroup.Prepend>
            <FormControl aria-label="Small" aria-describedby="inputGroup-sizing-sm" value={this.props.value} disabled={this.props.disabled} />
        </InputGroup>
        );
    }
}

export class ProfileData extends React.Component {
    render() {
        const data = this.props.data;
        if (this.props.type === "specialist") {
            return (
                <Container>
                    <DataLine title="Imię:" value={data.name} disabled={true}/>
                    <DataLine title="Nazwisko:" value={data.surname} disabled={true}/>
                    <DataLine title="Płeć:" value={data.gender} disabled={true}/>
                    <DataLine title="Email:" value={data.email} disabled={true}/>
                    <DataLine title="Telefon:" value={data.phoneNumber} disabled={true}/>
                    <DataLine title="Miasto:" value={data.city} disabled={false}/>
                    <DataLine title="Rola:" value={data.role} disabled={true}/>
                </Container>
            );
        } else {
            return (
                <Container>
                    <DataLine title="Imię:" value={data.name} disabled={true}/>
                    <DataLine title="Nazwisko:" value={data.surname} disabled={true}/>
                    <DataLine title="Płeć:" value={data.gender} disabled={true}/>
                    <DataLine title="Email:" value={data.email} disabled={true} />
                    <DataLine title="Telefon:" value={data.phoneNumber} disabled={true}/>
                    <DataLine title="Miasto:" value={data.city} disabled={false}/>
                    {/* <Col>
                        <div class="title">Imię:</div> <h4>{data.name}</h4>
                        <div class="title">Nazwisko:</div><h4> {data.surname}</h4>
                        <div class="title">Płeć:</div><h4> {data.gender}</h4>
                        <div class="title">Email:</div><h4> {data.email}</h4>
                        <div class="title">Telefon:</div><h4> {data.phoneNumber}</h4>
                        <div class="title">Miasto:</div> <h4>{data.city}</h4>
                    </Col> */}
                </Container>
            );
        }


    }
}