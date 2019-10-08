import React from 'react';

class Tables extends React.Component {

    constructor(props) {
        super(props);
    }

    renderTableData() {
        return this.props.tables.map((table) => {
            return (
                <tr key={table.name} >
                    <td onClick={() => this.props.loadColumns(table)} >{table.schema}.{table.name}</td>
                    <td><button onClick={() => this.props.createTable(table)}  className="btn btn-secondary btn-sm">Create</button></td>
                </tr>
            )
        })
    }

    render() {
        return (
            <table className='table table-striped table-hover' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Tables</th>
                    </tr>
                </thead>
                <tbody>
                    {this.renderTableData()}
                </tbody>
            </table>
        );
    }
}

export default Tables;
